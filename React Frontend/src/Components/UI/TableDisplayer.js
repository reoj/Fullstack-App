import React, { useContext, useState } from "react";
import OffcanvasHeader from "react-bootstrap/esm/OffcanvasHeader";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import AddUser from "../Models/Users/AddUser";
import AddItem from "../Models/Items/AddItem";
import ModalContext from "../../Context/UI/modal-context";
import CustomModal from "./CustomModal";
import EnactTrade from "../Models/Trades/EnactTrade";
import ModalContextHandler from "../../Context/ModalContextHandler";

function TableDisplayer(props) {
  const fielsList = props.colList;
  
  const cntx = useContext(ModalContext);
  const modalProperties = cntx.properties;
  const dsp = cntx.setter;

  function AddHandler() {
    if (props.modelType === "Users") {
      dsp({
        type: "OPEN",
        payload: { title: "Adding User", body: <AddUser /> },
      });
    }
    if (props.modelType === "Items") {
      dsp({
        type: "OPEN",
        payload: { title: "Adding Item", body: <AddItem /> },
      });
    }
    if (props.modelType === "Trades") {
      dsp({
        type: "OPEN",
        payload: { title: "New trade", body: <EnactTrade /> },
      });
    }
  }

  return (
    <div className="table-responsive">
        {modalProperties.onDisplay && (
          <CustomModal/>
        )}
        <OffcanvasHeader className="fs-3 mb-3 col-1 w-50">
          {props.modelType}
        </OffcanvasHeader>
        {props.modelType !== "Filtered Items" && (
          <Button className="col-2" onClick={AddHandler}>
            + Add {props.modelType}
          </Button>
        )}

        <Table
          className="table table-striped
            table-hover	
            table-borderless
            table-primary
            align-middle"
        >
          <thead key={props.modelType + "_thead"} className="table-light">
            <tr key={props.modelType + "_first_tr"}>
              {fielsList.map((campo) => {
                return (
                  <th key={props.modelType + Math.random().toString()}>
                    {campo}
                  </th>
                );
              })}
              <th colSpan="2">Actions</th>
            </tr>
          </thead>
          <tbody className="table-group-divider">{props.children}</tbody>
        </Table>
    </div>
  );
}

export default TableDisplayer;
