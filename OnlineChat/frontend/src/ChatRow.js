import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

class TableRow extends Component {

  constructor(props) {
        super(props);
    }
  render() {
    return (
        <tr>
          <td>
            {this.props.obj.name}
          </td>
        </tr>
    );
  }
}

export default TableRow;