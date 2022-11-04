import React, { Fragment } from "react";
import Navbar from "./navbar";
import MainDisplay from "./MainDisplay";

function Layout() {

  return (
    <Fragment>
      <Navbar />
      <MainDisplay></MainDisplay>
    </Fragment>
  );
}

export default Layout;
