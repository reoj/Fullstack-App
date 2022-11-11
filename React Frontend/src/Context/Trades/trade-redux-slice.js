import { createSlice } from "@reduxjs/toolkit";

export const tradeSlice = createSlice({
  name: "trades",
  initialState: {
    value: { list: [] },
  },
  reducers: {
    addTradeR: (state, action) => {
      const nwList = state.value.list;
      nwList.push({
        id: action.payload.id,
        sender: action.payload.sender,
        reciever: action.payload.reciever,
        itemName: action.payload.itemName,
        itemQuantity: action.payload.itemQuantity,
      });
      state.value.list = nwList;
    },
    removeTradeR: (state, action) => {
      const idToRemove = action.payload;
      const nwList = state.value.list;
      const index = state.value.list.findIndex((e) => {
        return e.id === idToRemove;
      });
      nwList.splice(index, 1);
      state.value.list = nwList;
    },
    refreshTrades: (state, action) => {
      state.value.list = action.payload;
    },
  },
});

export const { addTradeR, removeTradeR, refreshTrades } = tradeSlice.actions;
export default tradeSlice.reducer;
