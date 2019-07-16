import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

class ChatLabel extends Component {

  constructor(props) {
        super(props);
  }


  render() {
    return (
        <div class="form-check">
            <input type="radio" class="form-check-input" id={this.props.obj.name} name="name2"/>
            <label class="form-check-label" for={this.props.obj.name}>
                {this.props.obj.name} 
            </label>
        </div>
    );
  }
}

export default ChatLabel;