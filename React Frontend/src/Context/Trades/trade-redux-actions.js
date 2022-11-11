import { setErrorState, setLoadingState } from "../UI/fetching-state-redux-slice";
import { addTradeR, refreshTrades } from "./trade-redux-slice";

export function fetchInitialState() {
  return async (dispatch) => {
    dispatch(setErrorState(""));
    try {
      dispatch(setLoadingState(true));
      const response = await fetch("https://localhost:7158/Exchange/GetAll");

      if (!response.ok) {
        const data = await response.json()
        throw new Error(data.message);
      }

      const data = await response.json();

      dispatch(dispatch(setLoadingState(false)));

      dispatch(refreshTrades(data.body || []),);
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("Could not fetch data! " + error));
    }
  };
}

export function sendNewTrade(payload) {
  return async (dispatch) => {
    try {
      dispatch(setLoadingState(true));
      const response = await fetch("https://localhost:7158/Exchange/", {
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

      dispatch(addTradeR(data.body));
      dispatch(fetchInitialState());
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The Trade couldn't be added " + error));
    }
  };
}

export function sendDeleteTrade(payload) {
  return async (dispatch) => {
    try {
      const response = await fetch(
        "https://localhost:7158/Exchange/" + payload,
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

      dispatch(addTradeR(data.body.id));
      dispatch(fetchInitialState());
    } catch (error) {
      dispatch(setLoadingState(false));
      dispatch(setErrorState("The Trade couldn't be Deleted: " + error));
    }
  };
}
