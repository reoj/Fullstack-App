import { combineReducers } from "redux";
import itemSlice from "./Items/items-redux-slice";
import userSlice from "./Users/user-redux-slice";
import statusSlice from "./UI/fetching-state-redux-slice";
import tradeSlice from "./Trades/trade-redux-slice";

const rootReducer = combineReducers({
  user: userSlice,
  item: itemSlice,
  status: statusSlice,
  trades: tradeSlice,
});

export default rootReducer;
