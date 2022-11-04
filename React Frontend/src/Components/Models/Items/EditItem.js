import React, { useContext, useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import ModalContext from "../../../Context/modal-context";
import { useDispatch } from "react-redux";
import { editItemR } from "../../../Context/items-redux-slice";

function EditItem(props) {
  const modalCtx = useContext(ModalContext);
  const mc = modalCtx.setter;

  const ownerfieldRef = useRef();
  const descfieldRef = useRef()
  const stfieldRef = useRef()

  const dsp = useDispatch();

  function onCloseHandle(oldData) {
    mc({
      ...oldData,
      onDisplay: false,
    });
  }
  function onSaveHandle(oldData) {
    const emptyFields = checkNoNulls([descfieldRef, ownerfieldRef, stfieldRef]);
    if (emptyFields.length !== 0) {
      emptyFields.forEach((f) => {
        f.current.className = "form-control bg-danger";
      });
      return;
    }
    dsp(
      editItemR({
        id: props.item.id,
        desc: descfieldRef.current.value,
        owner: ownerfieldRef.current.value,
        state:stfieldRef.current.value
      })
    );
    onCloseHandle(oldData);
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
  function onInputClarity(event) {
    event.target.className = "form-control";
  }

  return (
    <Form>
      <Modal.Body>
        <Form.Group className="mb-3" controlId="form_Desc">
          <Form.Label>Description</Form.Label>
          <Form.Control
            type="text"
            placeholder="Item Description"
            ref={descfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.desc}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Owner">
          <Form.Label>State</Form.Label>
          <Form.Control
            type="text"
            placeholder="Item State"
            ref={stfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.st}
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="form_Owner">
          <Form.Label>Owner</Form.Label>
          <Form.Control
            type="text"
            placeholder="Numeric ID of owner"
            ref={ownerfieldRef}
            onFocus={onInputClarity}
            defaultValue={props.item.owner}
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
