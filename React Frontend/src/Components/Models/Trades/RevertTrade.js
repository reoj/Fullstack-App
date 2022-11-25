import React, { Fragment, useContext } from "react";
import { useDispatch } from "react-redux";
import ModalContext from "../../../Context/UI/modal-context";
import Table from "react-bootstrap/Table";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/esm/Button";
import { sendDeleteTrade } from "../../../Context/Trades/trade-redux-actions";

function RevertTrade(props) {
  const cTrade = props.item;

  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const dsp = useDispatch();

  function onCloseHandle(oldData) {
    modalDispatch({type:"CLOSE"})
  }
  function onSaveHandle(oldData) {
    dsp(sendDeleteTrade(JSON.parse(cTrade.id)));
    onCloseHandle(oldData);
  }

  return (
    <Fragment>
      <Modal.Body>
        <Table
          variant="primary"
          borderless={true}
          responsive={true}
          striped={true}
          bordered={true}
        >
          <tbody>
            <tr>
              <td className="fw-bold">ID: </td>
              <td>{cTrade.id}</td>
            </tr>
            <tr>
              <td className="fw-bold">Sender: </td>
              <td>{cTrade.sender}</td>
            </tr>
            <tr>
              <td className="fw-bold">Reciever: </td>
              <td>{cTrade.Reciever}</td>
            </tr>
            <tr>
              <td className="fw-bold">Item Name: </td>
              <td>{cTrade.itemName}</td>
            </tr>
            <tr>
              <td className="fw-bold">Quantity: </td>
              <td>{cTrade.itemQuantity}</td>
            </tr>
          </tbody>
        </Table>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="primary" onClick={onCloseHandle}>
          Close
        </Button>
        <Button variant="danger" onClick={onSaveHandle}>
          RevertTrade
        </Button>
      </Modal.Footer>
    </Fragment>
  );
}

export default RevertTrade;
