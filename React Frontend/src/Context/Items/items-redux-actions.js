import {
  refreshItems,
  addItemR,
  removeItemR,
  editItemR,
} from "./items-redux-slice";

import { setErrorState, setLoadingState } from "../UI/fetching-state-redux-slice";

export function fetchInitialState() {
  return async (dispatch) => {
    dispatch(setErrorState(""));
    try {
      dispatch(setLoadingState(true));
      const response = await fetch("https://localhost:7158/Inventory/GetAll");

      if (!response.ok) {
        throw new Error(response.statusText);
      }

      const data = await response.json();

      dispatch(setLoadingState(false));
      dispatch(refreshItems(data.body || []));
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("Could not fetch data! " + error));
    }
  };
}

export function sendNewItem(payload) {
  return async (dispatch) => {
    dispatch(setLoadingState(true));
    try {
      dispatch(setErrorState(""));
      const response = await fetch("https://localhost:7158/Inventory/", {
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

      dispatch(addItemR(data.body));
      dispatch(fetchInitialState());
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The Item couldn't be added: " + error));
    }
  };
}

export function sendDeleteItem(payload) {
  return async (dispatch) => {
    dispatch(setLoadingState(true));
    try {
      dispatch(setErrorState(""));
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
        const data = await response.json()
        throw new Error(data.message);
      }
      const data = await response.json();

      dispatch(removeItemR(data.body.id));
      dispatch(fetchInitialState());
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The Item couldn't be Deleted: " + error));
    }
  };
}

export function sendEditItem(payload) {
  return async (dispatch) => {
    dispatch(setLoadingState(true));
    try {
      dispatch(setErrorState(""));
      const response = await fetch("https://localhost:7158/Inventory/", {
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

      dispatch(editItemR(data.body));
      dispatch(fetchInitialState());
    } catch (error) {
      // "The Item couldn't be Edited"
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The Item couldn't be Edited: " + error));
    }
  };
}
