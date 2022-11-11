import { setErrorState, setLoadingState } from "../UI/fetching-state-redux-slice";
import {
  refreshUsers,
  addUserR,
  removeUser,
  editUserR,
} from "./user-redux-slice";

export function fetchInitialState() {
  return async (dispatch) => {
    dispatch(setErrorState(""));
    try {
      dispatch(setLoadingState(true));
      const response = await fetch("https://localhost:7158/Users/GetAll");

      if (!response.ok) {
        const data = await response.json()
        throw new Error(data.message);
      }

      const data = await response.json();

      dispatch(dispatch(setLoadingState(false)));

      dispatch(refreshUsers(data.body || []),);
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("Could not fetch data! " + error));
    }
  };
}

export function sendNewUser(payload) {
  return async (dispatch) => {
    try {
      dispatch(setLoadingState(true));
      const response = await fetch("https://localhost:7158/Users/", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        const data = await response.json()
        throw new Error(data.message);
      }
      const data = await response.json();

      dispatch(addUserR(data.body));
      dispatch(fetchInitialState());
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The User couldn't be added " + error));
    }
  };
}

export function sendDeleteUser(payload) {
  return async (dispatch) => {
    try {
      const response = await fetch(
        "https://localhost:7158/Users/" + JSON.stringify(payload),
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (!response.ok) {
        const data = await response.json()
        throw new Error(data.message);
      }
      const data = await response.json();

      dispatch(removeUser(data.body.id));
      dispatch(fetchInitialState());
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The User couldn't be Deleted: " + error));
    }
  };
}

export function sendEditUser(payload) {
  return async (dispatch) => {
    try {
      const response = await fetch("https://localhost:7158/Users/", {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        const data = await response.json()
        throw new Error(data.message);
      }
      const data = await response.json();

      dispatch(editUserR(data.body));
      dispatch(fetchInitialState());
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The User couldn't be Edited " + error));
    }
  };
}
