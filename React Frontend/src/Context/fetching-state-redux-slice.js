import { createSlice } from "@reduxjs/toolkit";

export const statusSlice = createSlice({
  name: "status",
  initialState: {
    value: { error: "", loading: false },
  },
  reducers: {
    setLoadingState: (state, action) => {
      state.value.loading = action.payload;
    },
    setErrorState: (state, action) => {
      state.value.loading = false;
      state.value.error = action.payload;
    },
  },
});

export const { setLoadingState, setErrorState } = statusSlice.actions;
export default statusSlice.reducer;
