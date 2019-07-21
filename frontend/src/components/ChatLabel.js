import React, { Component } from 'react'
import PropTypes from 'prop-types';

export class ChatLabel extends Component {
  render() {
    return this.props.options.map((obj, index) => (
      <div className="form-check" key={index}>
        <label className="form-check-label" key={index}>
          <input
            type="radio"
            className="form-check-input"
            id={obj.name}
            value={obj.name}
            key={index}
            checked={this.props.selected === obj.name}
            onChange={this.props.onChange}
          />
          {obj.name}
        </label>
      </div>
    ));
  }
}

ChatLabel.propTypes = {
  options: PropTypes.array.isRequired,
  selected: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
}

export default ChatLabel