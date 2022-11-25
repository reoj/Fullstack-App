import React, { useContext } from "react";
import Button from "react-bootstrap/esm/Button";
import ModalContext from "../../../Context/UI/modal-context";
import RevertTrade from "./RevertTrade";

function TradeSingle(props) {
  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const single = {
    id: props.idn,
    sender: props.sender,
    Reciever: props.Reciever,
    itemName: props.itemName,
    itemQuantity: props.itemQuantity,
  }

  function onDeleteHandler(params) {
    modalDispatch({
      type: "OPEN",
      payload: {
        title: "Please confirm that you wish to revert this trade",
        body: (
          <RevertTrade
            item={single}
          />
        ),
      },
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
      <td>
        <Button className="btn btn-danger" onClick={onDeleteHandler}>
          Revert
        </Button>
      </td>
    </tr>
  );
}

export default TradeSingle;
