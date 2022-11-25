import React, { useContext, useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import { useDispatch } from "react-redux";
import { sendNewItem } from "../../../Context/Items/items-redux-actions";
import ModalContext from "../../../Context/UI/modal-context";
import { checkNoNulls } from "../../Helpers/CheckNoNulls";

function AddItem(props) {
  const descfieldRef = useRef();
  const ownerfieldRef = useRef();
  const inamecfieldRef = useRef();
  const qtfieldRef = useRef();

  const dsp = useDispatch();

  const modalCtx = useContext(ModalContext);
  const modalController = modalCtx.setter;

  // Modal Acctions
  function onCloseHandle(oldData) {
    modalController({
      ...oldData,
      onDisplay: false,
    });
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
    // Call for Dispatch
    dsp(
      sendNewItem({
        name: inamecfieldRef.current.value,
        description: descfieldRef.current.value,
        quantity: qtfieldRef.current.value,
        userId: ownerfieldRef.current.value,
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
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Desc">
          <Form.Label>Description</Form.Label>
          <Form.Control
            type="text"
            placeholder="Item Description"
            ref={descfieldRef}
            onFocus={onInputClarity}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Owner">
          <Form.Label>Quantity</Form.Label>
          <Form.Control
            type="text"
            placeholder="Item Quantity"
            ref={qtfieldRef}
            onFocus={onInputClarity}
            defaultValue={""}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Owner">
          <Form.Label>Owner</Form.Label>
          <Form.Control
            type="text"
            placeholder="Numeric ID of owner"
            ref={ownerfieldRef}
            onFocus={onInputClarity}
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

export default AddItem;
