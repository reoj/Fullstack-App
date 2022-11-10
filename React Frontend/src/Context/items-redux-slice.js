import { createSlice } from "@reduxjs/toolkit";

export const itemSlice = createSlice({
  name: "items",
  initialState: {
    value: { list: [] },
  },
  reducers: {
    addItemR: (state, action) => {
      const nwList = state.value.list;
      nwList.push({
        id: action.payload.id,
        name: action.payload.name,
        description: action.payload.desc,
        quantity: action.payload.state,
        owner: action.payload.owner,
      });
      state.value.list = nwList;
    },
    removeItemR: (state, action) => {
      const idToRemove = action.payload;
      const nwList = state.value.list;
      const index = state.value.list.findIndex((e) => {
        return e.id === idToRemove;
      });
      nwList.splice(index, 1);
      state.value.list = nwList;
    },
    editItemR: (state, action) => {
      const idToEdit = action.payload.id;
      const oldItemIndex = state.value.list.findIndex((e) => {
        return e.id === idToEdit;
      });
      state.value.list[oldItemIndex] = action.payload;
    },
    refreshItems:(state, action)=>{
      state.value.list = action.payload
    }
  },
});


export const { addItemR, removeItemR, editItemR, refreshItems } =
  itemSlice.actions;
export default itemSlice.reducer;
