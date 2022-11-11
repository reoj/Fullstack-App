import { createSlice } from "@reduxjs/toolkit";

export const itemSlice = createSlice({
  name: "trades",
  initialState: {
    value: { list: [] },
  },
  reducers: {
    addTradeR: (state, action) => {
      const nwList = state.value.list;
      nwList.push({
        id: action.payload.id,
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
    editTradeR: (state, action) => {
      const idToEdit = action.payload.id;
      const oldItemIndex = state.value.list.findIndex((e) => {
        return e.id === idToEdit;
      });
      state.value.list[oldItemIndex] = action.payload;
    },
    refreshTrades: (state, action) => {
      state.value.list = action.payload;
    },
  },
});

export const { addItemR, removeItemR, editItemR, refreshItems } =
  itemSlice.actions;
export default itemSlice.reducer;
