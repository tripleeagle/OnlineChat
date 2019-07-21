import React, { Component } from "react";
import axios from "axios";
import * as signalR from "@aspnet/signalr";
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { fetchChats } from '../actions/chatActions';

import ChatLabel from "./ChatLabel"

class Chat extends Component {
  constructor(props) {
    super(props);

    this.state = {
      message: "",
      messages: [],
      chats: [],
      users: [],
      activeChat: "",
      activeUser: ""
    };

    this.hubConnection = null;
  }

  componentWillMount() {
    this.props.fetchChats();
  }

  componentDidMount = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/chathub", signalR.LogLevel.Trace)
      .build();
    this.hubConnection.on(
      "NewMessageNotification",
      this.onNewMessageNotification
    );
    this.hubConnection.on(
      "UserJoinChatNotification",
      this.onUserJoinChatNotification
    );
    this.hubConnection.on(
      "UserLeaveChatNotification",
      this.onUserLeaveChatNotification
    );
    this.hubConnection.on(
      "NewHistoryNotification",
      this.onNewHistoryNotification
    );

    this.hubConnection
      .start()
      .then(() => console.info("SignalR Connected"))
      .catch(err => console.error("SignalR Connection Error: ", err));

    axios
      .get("/api/user")
      .then(response => {
        this.setState({ users: response.data });
      })
      .catch(function(error) {
        console.log(error);
      });
  };

  onNewMessageNotification = (message) => {
    const text = `${message}`;
    const messages = this.state.messages.concat([text]);
    this.setState({ messages });
  }

  onUserJoinChatNotification = (nick) => {
    const text = `User ${nick} joined chat`;
    const messages = this.state.messages.concat([text]);
    this.setState({ messages });
  }

  onUserLeaveChatNotification = (nick) => {
    const text = `User ${nick} leaved chat`;
    const messages = this.state.messages.concat([text]);
    this.setState({ messages });
  }

  onNewHistoryNotification = (history) => {
    var messages = JSON.parse(history).Messages;
    if (messages == null) return;

    this.setState({ messages: [] });
    messages.map((message, index) => {
      const text = `${message.User.Name}: ${message.Text} (${message.CTime})`;
      const messages = this.state.messages.concat([text]);
      return this.setState({ messages });
    });
  }

  sendMessage = () => {
    this.hubConnection
      .invoke(
        "sendToAll",
        this.state.activeChat,
        this.state.activeUser,
        this.state.message
      )
      .catch(err => console.error(err));

    this.setState({ message: "" });
  };

  onStartChat = (e) => {
    this.hubConnection
      .invoke("joinChat", this.state.activeUser, this.state.activeChat)
      .catch(err => console.error(err));
  }

  handleSelectUser = (e) => {
    this.setState({ activeUser: e.target.value });
  }

  handleSelectChat = (e) => {
    this.setState({ activeChat: e.target.value });
  }

  render() {
    return (
      <div>
        <div className="row">
          <div className="col-md-4">
            <h4 align="center">Select a chat</h4>
            {console.log(this.props.chats)}
            <ChatLabel
              options={this.props.chats}
              onChange={e => this.handleSelectChat(e)}
              selected={this.state.activeChat}
            />
          </div>
          <div className="col-md-4">
            <h4 align="center">Select a user</h4>
            <ChatLabel
              options={this.state.users}
              onChange={e => this.handleSelectUser(e)}
              selected={this.state.activeUser}
            />
          </div>
          <div className="col-md-4">
            <h4 align="center">Press start chat!</h4>
            <button
              type="button"
              className="btn btn-outline-success"
              onClick={this.onStartChat}
            >
              Start
            </button>
          </div>
        </div>
        <div className="row">
          <br />
          <input
            type="text"
            value={this.state.message}
            onChange={e => this.setState({ message: e.target.value })}
          />
          <button onClick={this.sendMessage}>Send</button>
        </div>
        <div>
          {this.state.messages.map((message, index) => (
            <span key={index}>{message}</span>
          ))}
        </div>
      </div>
    );
  }
}

Chat.propTypes = {
  fetchChats: PropTypes.func.isRequired,
  chats: PropTypes.array.isRequired
};

const mapStateToProps = state => ({
  chats: state.chats.items
});

export default connect(mapStateToProps, { fetchChats })(Chat);


