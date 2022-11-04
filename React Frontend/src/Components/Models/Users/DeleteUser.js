import React, { Fragment, useContext } from "react";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import ModalContext from "../../../Context/modal-context";
import { useDispatch } from "react-redux";
import { removeUser } from "../../../Context/user-redux-slice";
import Table from "react-bootstrap/Table";

function DeleteUser(props) {
  const modalCtx = useContext(ModalContext);
  const mc = modalCtx.setter;

  const dsp = useDispatch();

  function onCloseHandle(oldData) {
    mc({
      ...oldData,
      onDisplay: false,
    });
  }
  function onSaveHandle(oldData) {
    dsp(removeUser(props.item.id));
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
              <td className='fw-bold'>Name: </td>
              <td >{props.item.name}</td>
            </tr>
            <tr>
              <td className='fw-bold'>Class: </td>
              <td >{props.item.cl}</td>
            </tr>
          </tbody>
        </Table>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="primary" onClick={onCloseHandle}>
          Close
        </Button>
        <Button variant="danger" onClick={onSaveHandle}>
          DeleteUser
        </Button>
      </Modal.Footer>
    </Fragment>
  );
}

export default DeleteUser;
