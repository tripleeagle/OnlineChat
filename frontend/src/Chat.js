import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';


class Chat extends Component {
  constructor(props) {
    super(props);

    this.state = {
      nick: '',
      message: '',
      messages: []
    };
    this.hubConnection = null;
    this.onNotifReceived = this.onNotifReceived.bind(this);
  }

  componentDidMount = () => {
    const nickConst = window.prompt('Your name:', 'John');

    this.hubConnection = new signalR.HubConnectionBuilder().withUrl('https://localhost:5001/chathub', signalR.LogLevel.Trace).build();
    this.setState({nick: nickConst});
    //this.hubConnection.on('sendToAll',this.onNotifReceived);
    
    this.hubConnection.on('sendToAll', (nick, receivedMessage) => {
      const text = `${nick}: ${receivedMessage}`;
      const messages = this.state.messages.concat([text]);
      this.setState({ messages });
    });

    this.hubConnection.start()
    .then(() => console.info('SignalR Connected'))
    .catch(err => console.error('SignalR Connection Error: ', err));

  };

  onNotifReceived (res) {
    console.info('Yayyyyy, I just received a notification!!!', res);
  }

  sendMessage = () => {
    this.hubConnection
      .invoke('sendToAll', this.state.nick, this.state.message)
      .catch(err => console.error(err));

      this.setState({message: ''});      
  };

  render() {
    return (
      <div>
        <br />
        <input
          type="text"
          value={this.state.message}
          onChange={e => this.setState({ message: e.target.value })}
        />

        <button onClick={this.sendMessage}>Send</button>

        <div>
          {this.state.messages.map((message, index) => (
            <span style={{display: 'block'}} key={index}> {message} </span>
          ))}
        </div>
      </div>
    );
  }
}

export default Chat;