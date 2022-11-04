import React, { Fragment } from "react";
import { Provider } from "react-redux";
import { Route } from "react-router-dom";
import Container from "react-bootstrap/Container";
import UsersList from "../Models/Users/UsersList";
import ItemsList from "../Models/Items/ItemsList";
import FilteredItems from "../Models/Items/FilteredItems";


import store from "../../Context/store-redux"
import Landing from "./Landing";

function MainDisplay() {

  return (
    <Fragment>
      <Container className="w-75 rounded bg-light p-3 my-3">
        <Provider store={store}>
          <Route path="/" exact>
            <Landing/>
          </Route>
          <Route path="/Users">
            <UsersList />
          </Route>
          <Route path="/Items">
            <ItemsList/>
          </Route>
          <Route path="/Filtered-items/:userId">
            <FilteredItems/>
          </Route>
        </Provider>
      </Container>
    </Fragment>
  );
}

export default MainDisplay;
