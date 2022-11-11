import { createSlice } from "@reduxjs/toolkit";

export const userSlice = createSlice({
  name: "users",
  initialState: {
    value: { list: [] },
  },
  reducers: {
    addUserR: (state, action) => {
      const nwList = state.value.list;
      nwList.push({
        id: action.payload.id,
        name: action.payload.name,
        userType: action.payload.userType,
        email: action.payload.email,
        phone: action.payload.phone
      });
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
    refreshUsers: (state, action) => {
      state.value.list = action.payload;
    },
  },
});
export const { addUserR, removeUser, editUserR, refreshUsers } =
  userSlice.actions;
export default userSlice.reducer;
