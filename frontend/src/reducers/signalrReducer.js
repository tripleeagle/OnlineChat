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
        case messageTypes.NEW_HISTORY_NOTIFICATION:
            return {
                ...state,
                items: [...state.items,...getFormattedMessages(action.payload)]
            };
        case messageTypes.CLEAR_LOCAL_MESSAGES:
            console.log("CLEAR_LOCAL_MESSAGES");
            return {
                ...state,
                items: []
            };
        default:
            return state;
    }
}

function getFormattedMessages (messages) {
    let messagesJson = JSON.parse(messages).Messages;
    if (messagesJson == null) return;

    let formattedMessages = []
    messagesJson.map(message => {
        const text = `${message.User.Name}: ${message.Text} (${message.CTime})`;
        console.log(text);
        formattedMessages = formattedMessages.concat([text]);
    });
    return formattedMessages
}
