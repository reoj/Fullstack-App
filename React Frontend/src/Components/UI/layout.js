import React, { Fragment } from "react";
import Navbar from "./navbar";
import MainDisplay from "./MainDisplay";
import StatusCard from "./Status-Card";
import store from "../../Context/store-redux";
import { Provider } from "react-redux";

function Layout() {
  return (
    <Fragment>
      <Navbar />
      <Provider store={store}>
        <MainDisplay />
        <StatusCard />
      </Provider>
    </Fragment>
  );
}

export default Layout;
