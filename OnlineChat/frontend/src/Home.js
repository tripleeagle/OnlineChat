import React, { Component } from 'react';
import axios from 'axios';
import ChatRow from './ChatRow';

export default class Index extends Component {
    constructor(props) {
        super(props);
        this.state = {business: []};
      }
      componentDidMount(){
        axios.get( '/api/chat')
        .then(response => {
            this.setState({ business: response.data });
          })
        .catch(function (error) {
            console.log(error);
        })
      }
      tabRow(){
        return this.state.business.map(function(object, i){
            return <ChatRow obj={object} key={i} />;
        });
      }
  
      render() {
        return (
          <div>
            <h3 align="center">Business List</h3>
            <table className="table table-striped" style={{ marginTop: 20 }}>
              <thead>
                <tr>
                  <th>Chat name</th>
                </tr>
              </thead>
              <tbody>
                { this.tabRow() }
              </tbody>
            </table>
          </div>
        );
      }
}