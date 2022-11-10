import { refreshUsers } from "./user-redux-slice";

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