import React, { Component } from "react";
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { fetchChats } from '../actions/chatActions';
import { sendMessage, joinChat } from "../actions/signalrActions"
import Users from "./Users"
import ChatLabel from "./ChatLabel"

class Chat extends Component {
  constructor(props) {
    super(props);

    this.state = {
      message: "",
      activeChat: "",
      activeUser: ""
    };
  }

  componentWillMount() {
      this.props.fetchChats()
  }


  handleSelectUser = (e) => {
    this.setState({ activeUser: e.target.value });
  };

  handleSelectChat = (e) => {
    this.setState({ activeChat: e.target.value });
  };

  onJoinChat = () => {
      this.props.sendMessage(this.state.activeChat,this.state.activeUser,this.state.message);
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
              <Users onChange={e => this.handleSelectUser(e)} activeUser={this.state.activeUser}/>
          </div>
          <div className="col-md-4">
            <h4 align="center">Press start chat!</h4>
            <button
              type="button"
              className="btn btn-outline-success"
              onClick={() => this.props.joinChat(this.state.activeUser,this.state.activeChat)}
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
          <button onClick={() => this.onJoinChat}>Send</button>
        </div>
        <div>
          {this.props.messages.map((message, index) => (
            <span key={index}>{message}</span>
          ))}
        </div>
      </div>
    );
  }
}

Chat.propTypes = {
    fetchChats: PropTypes.func.isRequired,
    sendMessage: PropTypes.func.isRequired,
    joinChat: PropTypes.func.isRequired,
    chats: PropTypes.array.isRequired,
    messages: PropTypes.array.isRequired
};

const mapStateToProps = state => ({
    chats: state.chats.items,
    messages: state.messages.items
});

export default connect(mapStateToProps, { fetchChats, sendMessage, joinChat })(Chat);


