import React, { useContext, useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import ModalContext from "../../../Context/UI/modal-context";
import { useDispatch } from "react-redux";
import { sendEditItem } from "../../../Context/Items/items-redux-actions";
import { checkNoNulls } from "../../Helpers/CheckNoNulls";

function EditItem(props) {
  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const descfieldRef = useRef();
  const ownerfieldRef = useRef();
  const inamecfieldRef = useRef();
  const qtfieldRef = useRef();

  const dsp = useDispatch();

  const stGuID = (props.item.id).toString();


  function onCloseHandle(oldData) {
    modalDispatch({type:"CLOSE"})
  }
  function onSaveHandle(oldData) {
    const emptyFields = checkNoNulls([
      descfieldRef,
      ownerfieldRef,
      inamecfieldRef,
      qtfieldRef,
    ]);
    if (emptyFields.length !== 0) {
      emptyFields.forEach((f) => {
        f.current.className = "form-control bg-danger";
      });
      return;
    }
    dsp(
      sendEditItem({
        ItemId: stGuID,
        Name: inamecfieldRef.current.value,
        Description: descfieldRef.current.value,
        Quantity: qtfieldRef.current.value,
        UserId: ownerfieldRef.current.value,
      })
    );
    onCloseHandle(oldData);
  }
  function onInputClarity(event) {
    event.target.className = "form-control";
  }

  return (
    <Form>
      <Modal.Body>
      <Form.Group className="mb-3" controlId="form_Name">
          <Form.Label>Item Name</Form.Label>
          <Form.Control
            type="text"
            placeholder="Item Name"
            ref={inamecfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.name}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Desc">
          <Form.Label>Description</Form.Label>
          <Form.Control
            type="text"
            placeholder="Item Description"
            ref={descfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.description}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Owner">
          <Form.Label>Quantity</Form.Label>
          <Form.Control
            type="text"
            placeholder="Item Quantity"
            ref={qtfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.quantity}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Owner">
          <Form.Label>Owner</Form.Label>
          <Form.Control
            type="text"
            placeholder="Numeric ID of owner"
            ref={ownerfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.userId}
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

export default EditItem;
