import React, { Fragment } from "react";
import {NavLink } from "react-router-dom";

function Navbar() {
  return (
    <Fragment>
      <ul className="nav justify-content-center shadow bg-light fs-5">
        <li className="nav-item">
          <NavLink className="nav-link" to="/Users">Users</NavLink>
        </li>
        <li className="nav-item">
          <NavLink className="nav-link" to="/Items">Items</NavLink>
        </li>
      </ul>
    </Fragment>
  );
}

export default Navbar;
