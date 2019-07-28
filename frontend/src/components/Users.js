import React, { Component } from 'react'
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { fetchUsers } from '../actions/userActions';
import ChatLabel from './ChatLabel'

export class Users extends Component {
    componentWillMount() {
        this.props.fetchUsers();
    }

    render() {
        return (
            <React.Fragment>
                <h4 align="center">Select a user</h4>
                {console.log("users", this.props.users)}
                <ChatLabel
                    options={this.props.users}
                    onChange={e => this.props.onChange(e)}
                    selected={this.props.activeUser}
                />
            </React.Fragment>
        )
    }
}


Users.propTypes = {
    fetchUsers: PropTypes.func.isRequired,
    users: PropTypes.array.isRequired,
    activeUser: PropTypes.string.isRequired,
    onChange: PropTypes.func.isRequired
};

const mapStateToProps = state => ({
    users: state.users.items
});

export default connect(mapStateToProps, { fetchUsers })(Users);