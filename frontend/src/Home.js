import React, { Component } from 'react';
import axios from 'axios';
import Chat from './Chat'

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

export default class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {chats: [], users: [], activeChat: '', activeUser: ''};
        this.onStartChat = this.onStartChat.bind(this);
        this.handleSelectChat = this.handleSelectChat.bind(this);
      }
      componentDidMount(){
        axios.get( '/api/chat')
        .then(response => {
            this.setState({ chats: response.data });
          })
        .catch(function (error) {
            console.log(error);
        })

        axios.get( '/api/user')
        .then(response => {
            this.setState({ users: response.data });
          })
        .catch(function (error) {
            console.log(error);
        })
      }

      onStartChat(e){
        console.log("Clicked");
       
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
            <Chat />
          </div>
           
        );
      }
}