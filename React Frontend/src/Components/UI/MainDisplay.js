import React, { Fragment } from "react";

import { Route } from "react-router-dom";
import Container from "react-bootstrap/Container";
import UsersList from "../Models/Users/UsersList";
import ItemsList from "../Models/Items/ItemsList";
import FilteredItems from "../Models/Items/FilteredItems";

import Landing from "./Landing";
import TradesList from "../Models/Trades/TradesList";
import ModalContextHandler from "../../Context/ModalContextHandler";

function MainDisplay() {
  return (
    <Fragment>
      <ModalContextHandler>
        <Container className="w-75 rounded bg-light p-3 my-3">
          <Route path="/" exact>
            <Landing />
          </Route>
          <Route path="/Users">
            <UsersList />
          </Route>
          <Route path="/Items">
            <ItemsList />
          </Route>
          <Route path="/Filtered-items/:userId">
            <FilteredItems />
          </Route>
          <Route path="/Trades">
            <TradesList />
          </Route>
        </Container>
      </ModalContextHandler>
    </Fragment>
  );
}

export default MainDisplay;
