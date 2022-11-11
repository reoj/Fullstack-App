import {
  refreshItems,
  addItemR,
  removeItemR,
  editItemR,
} from "./items-redux-slice";

export function fetchInitialState() {
  return async (dispatch) => {
    const response = await fetch("https://localhost:7158/Inventory/GetAll");

    if (!response.ok) {
      throw new Error("Could not fetch data!");
    }

    const data = await response.json();

    dispatch(refreshItems(data.body || []));
  };
}

export function sendNewItem(payload) {
  return async (dispatch) => {
    const response = await fetch("https://localhost:7158/Inventory/", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      throw new Error("The Item couldn't be added");
    }
    const data = await response.json();

    dispatch(addItemR(data.body));
  };
}

export function sendDeleteItem(payload) {
  return async (dispatch) => {
    const response = await fetch(
      "https://localhost:7158/Inventory/" + JSON.stringify(payload),
      {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    if (!response.ok) {
      throw new Error("The Item couldn't be Deleted");
    }
    const data = await response.json();

    dispatch(removeItemR(data.body.id));
  };
}

export function sendEditItem(payload) {
  return async (dispatch) => {
    const response = await fetch(
      "https://localhost:7158/Inventory/",
      {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      }
    );

    if (!response.ok) {
      throw new Error("The Item couldn't be Edited");
    }
    const data = await response.json();

    dispatch(editItemR(data.body.itemId));
  };
}
