import * as messageTypes  from '../actions/types';

const initialState = {
    items: []
};

export default function(state = initialState, action) {
    switch (action.type) {
        case messageTypes.NEW_MESSAGE_NOTIFICATION:
            return {
                ...state,
                items: [...state.items, action.payload]
            };
        case messageTypes.USER_JOIN_CHAT_NOTIFICATION:
            console.log("User " + action.payload + " joined chat")
            return {
                ...state,
                items: [...state.items, "User " + action.payload + " joined chat"]
            };
        case messageTypes.USER_LEAVE_CHAT_NOTIFICATION:
            return {
                ...state,
                items: [...state.items, "User " + action.payload + " leaved chat"]
            };
        /*case messageTypes.NEW_HISTORY_NOTIFICATION:
        return {
            ...state,
            items: action.payload
        };*/
        default:
            return state;
    }
}
