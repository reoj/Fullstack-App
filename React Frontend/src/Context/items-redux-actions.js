import { refreshItems } from "./items-redux-slice";

export function fetchInitialState() {
  return async (dispatch) => {
    const response = await fetch("https://localhost:7158/Inventory/GetAll");

    if (!response.ok) {
      throw new Error("Could not fetch cart data!");
    }

    const data = await response.json();

    dispatch(refreshItems(data.body || []));
  };
}

export function sendNewItem(){
    return async (dispatch) => {
        // const response = await fetch("https://localhost:7158/Inventory/GetAll");
    }
}
