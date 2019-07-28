import { FETCH_CHATS } from "./types";
import axios from "axios";

export const fetchChats = () => dispatch => {
    console.log("fetchChats")
    axios.get("/api/chat")
    .then(response => 
        dispatch({
            type: FETCH_CHATS,
            payload: response.data
        })
    ).catch(function(error) {
        console.log(error);
    });
};

