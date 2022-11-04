import { createSlice } from "@reduxjs/toolkit";

export const userSlice = createSlice({
  name: "users",
  initialState: {
    value: { list: [{ id: 1, name: "Isra", utype:"Soldier" }, 
    { id: 2, name: "Jon", utype:"Thief " }] }
  },
  reducers: {
    addUserR: (state, action) => {
      const next = state.value.list.length + 1;
      const nwList = state.value.list;
      nwList.push({ id: next, name: action.payload.name, utype:action.payload.cl });
      state.value.list = nwList;
    },
    removeUser: (state, action) => {
      const idToRemove = action.payload;
      const nwList = state.value.list;
      const index = state.value.list.findIndex((e) => {
        return e.id === idToRemove;
      });
      nwList.splice(index, 1);
      state.value.list = nwList;
    },
    editUserR: (state, action) => {
      const idToEdit = action.payload.id;
      const oldItemIndex = state.value.list.findIndex((e) => {
        return e.id === idToEdit;
      });
      state.value.list[oldItemIndex] = action.payload;
    },
  },
});
export const { addUserR, removeUser, editUserR } = userSlice.actions;
export default userSlice.reducer;
