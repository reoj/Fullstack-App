import { refreshUsers, addUserR, removeUser } from "./user-redux-slice";

export function fetchInitialState() {
  return async (dispatch) => {
    const response = await fetch("https://localhost:7158/Users/GetAll");

    if (!response.ok) {
      throw new Error("Could not fetch cart data!");
    }

    const data = await response.json();

    dispatch(refreshUsers(data.body || []));
  };
}

export function sendNewUser(payload) {
  return async (dispatch) => {
    const response = await fetch("https://localhost:7158/Users/", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify( payload ),
    });

    if (!response.ok) {
      throw new Error("The User couldn't be added");
    }
    const data = await response.json();

    dispatch(addUserR(data.body));
  };
}

export function sendDeleteUser(payload) {
  return async (dispatch) => {
    const response = await fetch("https://localhost:7158/Users/"+JSON.stringify( payload ), {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error("The User couldn't be Deleted");
    }
    const data = await response.json();

    dispatch(removeUser(data.body.id));
  };
}
