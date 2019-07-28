import * as signalR from "@aspnet/signalr";
import * as messageTypes from "../actions/types";

let _hub;
export default function signalrMiddleware(store) {
    return (next) => async (action) => {
         if (_hub === undefined){
             connect(store);
         }
         console.log(action.type);
         switch(action.type){
             case messageTypes.SEND_MESSAGE:
                 _hub.invoke("sendToAll", action.chat, action.user, action.message)
                     .catch(err => console.error(err));
                 break;
             case messageTypes.JOIN_CHAT:
                 _hub.invoke("joinChat", action.user, action.chat)
                     .catch(err => console.error(err));
                 break;
             default:
                 return next(action);
         }
    }
}

function connect (store){
    _hub = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:5001/chathub", signalR.LogLevel.Trace)
        .build();
    _hub.on("NewMessageNotification", message => {
        store.dispatch({
            type: messageTypes.NEW_MESSAGE_NOTIFICATION,
            payload: message
        });
    });
    _hub.on("UserJoinChatNotification", message => {
        store.dispatch({
            type: messageTypes.USER_JOIN_CHAT_NOTIFICATION,
            payload: message
        });
    });
    _hub.on("UserLeaveChatNotification", message => {
        store.dispatch({
            type: messageTypes.USER_LEAVE_CHAT_NOTIFICATION,
            payload: message
        });
    });
    _hub.on("NewHistoryNotification", message => {
        store.dispatch({
            type: messageTypes.NEW_HISTORY_NOTIFICATION,
            payload: message
        });
    });
    _hub
        .start()
        .then(() => console.info("SignalR Connected"))
        .catch(err => console.error("SignalR Connection Error: ", err));
}