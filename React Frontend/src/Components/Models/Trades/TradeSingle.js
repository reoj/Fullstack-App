import React, { useContext } from "react";
import Button from "react-bootstrap/esm/Button";
import ModalContext from "../../../Context/UI/modal-context";
import RevertTrade from "./RevertTrade";

function TradeSingle(props) {
  const context = useContext(ModalContext);
  const mc = context.setter;

  function onDeleteHandler(params) {
    mc({
      onDisplay: true,
      title: "Please confirm that you wish to revert this trade",
      body: (
        <RevertTrade
          item={{
            id: props.idn,
            sender: props.sender,
            Reciever: props.Reciever,
            itemName: props.itemName,
            itemQuantity: props.itemQuantity,
          }}
        />
      ),
    });
  }
  return (
    <tr>
      <td>{props.idn}</td>
      <td>{props.sender}</td>
      <td>{props.Reciever}</td>
      <td>{props.itemName}</td>
      <td>{props.itemDescription}</td>
      <td>{props.itemQuantity}</td>
      <td><Button className="btn btn-danger" onClick={onDeleteHandler}>Revert</Button></td>
    </tr>
  );
}

export default TradeSingle;
