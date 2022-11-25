import React, { Fragment, useContext } from "react";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import ModalContext from "../../../Context/UI/modal-context";
import { useDispatch } from "react-redux";
import Table from "react-bootstrap/Table";
import { sendDeleteItem } from "../../../Context/Items/items-redux-actions";

function DeleteItem(props) {
  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const dsp = useDispatch();

  function onCloseHandle(oldData) {
    modalDispatch({type:"CLOSE"})
  }
  function onSaveHandle(oldData) {
    dsp(sendDeleteItem(props.item.id));
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
            <tr >
              <td className='fw-bold'>ID: </td>
              <td >{props.item.id}</td>
            </tr>
            <tr>
              <td className='fw-bold'>Description: </td>
              <td >{props.item.description}</td>
            </tr>
            <tr>
              <td className='fw-bold'>Owner: </td>
              <td >{props.item.userId}</td>
            </tr>
          </tbody>
        </Table>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="primary" onClick={onCloseHandle}>
          Close
        </Button>
        <Button variant="danger" onClick={onSaveHandle}>
          Delete Item
        </Button>
      </Modal.Footer>
    </Fragment>
  );
}

export default DeleteItem;
