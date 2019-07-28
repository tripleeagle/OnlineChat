import { FETCH_USERS } from "./types";
import axios from "axios";

export const fetchUsers = () => dispatch => {
    axios
        .get("/api/user")
        .then(response =>
            dispatch({
                type: FETCH_USERS,
                payload: response.data
            })
        )
        .catch(function(error) {
            console.log(error);
        });
};