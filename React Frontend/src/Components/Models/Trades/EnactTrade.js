import React, { useContext, useRef } from "react";
import Modal from "react-bootstrap/Modal";
import Form from "react-bootstrap/Form";
import ModalContext from "../../../Context/UI/modal-context";
import { useDispatch } from "react-redux";
import Button from "react-bootstrap/esm/Button";
import { sendNewTrade } from "../../../Context/Trades/trade-redux-actions";

function EnactTrade() {
  const modalCtx = useContext(ModalContext);
  const mc = modalCtx.setter;

  const senderfieldRef = useRef();
  const recieverfieldRef = useRef();
  const iNamefieldRef = useRef();
  const idescfieldRef = useRef();
  const iQuantfieldRef = useRef();

  const dsp = useDispatch();

  function onCloseHandle(oldData) {
    mc({
      ...oldData,
      onDisplay: false,
    });
  }
  function checkNoNulls(arrayOfRefs) {
    const allEmptyFields = [];
    arrayOfRefs.forEach((r) => {
      if (r.current.value === "") {
        allEmptyFields.push(r);
      }
    });
    return allEmptyFields;
  }

  function onSaveHandle(oldData) {
    const emptyFields = checkNoNulls([
        senderfieldRef,
        recieverfieldRef,
        iNamefieldRef,
        idescfieldRef,
        iQuantfieldRef,
    ]);
    if (emptyFields.length !== 0) {
      emptyFields.forEach((f) => {
        f.current.className = "form-control bg-danger";
      });
      return;
    }
    dsp(
      sendNewTrade({
        sender: senderfieldRef.current.value,
        reciever: recieverfieldRef.current.value,
        itemName: iNamefieldRef.current.value,
        itemDescription: idescfieldRef.current.value,
        itemQuantity: iQuantfieldRef.current.value,
      })
    );
    onCloseHandle(oldData);
  }

  function onInputClarity(event) {
    // event..className = "form-control"
    event.target.className = "form-control";
  }
  return (
    <Form>
      <Modal.Body>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Sender</Form.Label>
          <Form.Control
            type="text"
            placeholder="Numeric ID of the Sender"
            ref={senderfieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Reciever</Form.Label>
          <Form.Control
            type="text"
            placeholder="Numeric ID of the Sender"
            ref={recieverfieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Item Name</Form.Label>
          <Form.Control
            type="text"
            placeholder="Name of the Item to Trade"
            ref={iNamefieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Item Description</Form.Label>
          <Form.Control
            type="text"
            placeholder="Description of the Item"
            ref={idescfieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Quantity</Form.Label>
          <Form.Control
            type="text"
            placeholder="How many Items should be sent?"
            ref={iQuantfieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onCloseHandle}>
          Close
        </Button>
        <Button variant="primary" onClick={onSaveHandle}>
          Save changes
        </Button>
      </Modal.Footer>
    </Form>
  );
}

export default EnactTrade;
