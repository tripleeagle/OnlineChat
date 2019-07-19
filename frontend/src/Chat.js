import React, { Component } from 'react';
import axios from 'axios';
import * as signalR from '@aspnet/signalr';

const ChatInputLabel = ({ options, selected, onChange }) => {
  return (
      options.map((obj, index) => (
        <div className="form-check" key={index}>
          <label className="form-check-label" key={index}>
            <input type="radio" className="form-check-input" 
              id={obj.name} 
              value={obj.name}
              key={index}
              checked={selected === obj.name}
              onChange={onChange} />
            {obj.name}
          </label>
        </div>
      ))
  );
}

class Chat extends Component {
  constructor(props) {
    super(props);

    this.state = {
      message: '',
      messages: [],
      chats: [], 
      users: [], 
      activeChat: '', 
      activeUser: ''
    };

    this.hubConnection = null;
    this.onNewMessageNotification = this.onNewMessageNotification.bind(this);
    this.onUserJoinChatNotification = this.onUserJoinChatNotification.bind(this);
    this.onUserLeaveChatNotification = this.onUserLeaveChatNotification.bind(this);
    this.onNewHistoryNotification = this.onNewHistoryNotification.bind(this);

    this.onStartChat = this.onStartChat.bind(this);
    this.handleSelectChat = this.handleSelectChat.bind(this);
  }

  componentDidMount = () => {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl('https://localhost:5001/chathub', signalR.LogLevel.Trace).build();    
    this.hubConnection.on('NewMessageNotification', this.onNewMessageNotification);
    this.hubConnection.on('UserJoinChatNotification', this.onUserJoinChatNotification);
    this.hubConnection.on('UserLeaveChatNotification', this.onUserLeaveChatNotification);
    this.hubConnection.on('NewHistoryNotification', this.onNewHistoryNotification);

    this.hubConnection.start()
    .then(() => console.info('SignalR Connected'))
    .catch(err => console.error('SignalR Connection Error: ', err));

    axios.get( '/api/chat').then(response => {
      this.setState({ chats: response.data });
    }).catch(function (error) {
        console.log(error);
    })

    axios.get( '/api/user').then(response => { 
      this.setState({ users: response.data });
    }).catch(function (error) {
        console.log(error);
    })
  };

  onNewMessageNotification (message) {
    console.log("onNewMessageNotification"); 
    //const text = `${message.userId}: ${message.text} (${message.cTime})`;
    const text = `${message}`
    const messages = this.state.messages.concat([text]);
    this.setState({ messages });
  }

  onUserJoinChatNotification (nick){
    console.log("onUserJoinChatNotification"); 
    const text = `User ${nick} joined chat`;
    const messages = this.state.messages.concat([text]);
    this.setState({ messages });
  }

  onUserLeaveChatNotification (nick){
    console.log("onUserLeaveChatNotification"); 
    const text = `User ${nick} leaved chat`;
    const messages = this.state.messages.concat([text]);
    this.setState({ messages });
  }

  onNewHistoryNotification(history){
    console.log("onNewHistoryNotification"); 
    history.map((message, index) => {
      const text = `${message.userId}: ${message.text} (${message.cTime})`;
      const messages = this.state.messages.concat([text]);
      this.setState({ messages });
    });
  }

  sendMessage = () => {
    console.log("sendMessage"); 
    this.hubConnection
      .invoke('sendToAll', this.state.activeChat, this.state.activeUser, this.state.message)
      .catch(err => console.error(err));

      this.setState({message: ''});      
  };

  onStartChat(e){
    console.log("onStartChat"); 
    this.hubConnection
      .invoke('joinChat', this.state.activeUser, this.state.activeChat)
      .catch(err => console.error(err));
  }

  handleSelectUser(e){
    console.log("selected user", e.target.value); 
    this.setState({ activeUser: e.target.value});
  }

  handleSelectChat(e){
    console.log("selected chat", e.target.value); 
    this.setState({ activeChat: e.target.value});
  }

  render() {
    return (
      <div>
        <div className="row">
            <div className="col-md-4">
                <h4 align="center">Select a chat</h4>
                <ChatInputLabel 
                  options={this.state.chats}
                  onChange={(e) => this.handleSelectChat(e)}
                  selected={this.state.activeChat} 
                />
            </div>
            <div className="col-md-4">
                <h4 align="center">Select a user</h4>
                <ChatInputLabel 
                  options={this.state.users}
                  onChange={(e) => this.handleSelectUser(e)}
                  selected={this.state.activeUser} 
                />
            </div>
            <div className="col-md-4">
                <h4 align="center">Press start chat!</h4>
                <button type="button" className="btn btn-outline-success" onClick={this.onStartChat}>Start</button>
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

export default Chat;