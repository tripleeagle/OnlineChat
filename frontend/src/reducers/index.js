import { combineReducers } from 'redux';
import chatReducer from './chatReducer';
import userReducer from './userReducer';
import signalrReducer from "./signalrReducer";

export default combineReducers({
    chats: chatReducer,
    users: userReducer,
    messages: signalrReducer
});