import * as messageTypes from "./types"

export const sendMessage  = (chat, user, message) => dispatch => {
    dispatch({
        type : messageTypes.SEND_MESSAGE,
        chat,
        user,
        message
    })
};


export const joinChat  = (user,chat) => dispatch => {
    dispatch({
        type: messageTypes.JOIN_CHAT,
        user,
        chat
    })
};

