import React, { useContext, useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import Modal from "react-bootstrap/Modal";
import { sendNewItem } from "../../../Context/Items/items-redux-actions";
import ModalContext from "../../../Context/UI/modal-context";
import CustomFormGroup from "../../UI/CustomFormGroup";
import { useDispatch } from "react-redux";
import { checkNoNulls } from "../../Helpers/CheckNoNulls";

function AddItem(props) {
  const descfieldRef = useRef();
  const ownerfieldRef = useRef();
  const inamecfieldRef = useRef();
  const qtfieldRef = useRef();

  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const dsp = useDispatch();

  const arrayOfRefs = [
    descfieldRef,
    ownerfieldRef,
    inamecfieldRef,
    qtfieldRef,
  ];

  // Modal Acctions
  function onCloseHandle() {
    modalDispatch({ type: "CLOSE" });
  }

  function onSaveHandle() {
    const emptyFields = checkNoNulls(arrayOfRefs);
    if (emptyFields.length !== 0) {
      emptyFields.forEach((f) => {
        f.current.className = "form-control bg-danger";
      });
      return;
    }
    const dataForDispatch = {
      name: inamecfieldRef.current.value,
      description: descfieldRef.current.value,
      quantity: qtfieldRef.current.value,
      userId: ownerfieldRef.current.value,
    };

    // Call for Dispatch
    dsp(
      sendNewItem(dataForDispatch)
    );
    onCloseHandle();
  }
  
  return (
    <Form>
      <Modal.Body>
        <CustomFormGroup
          controlId="form_Name"
          label="Item Name"
          placeholder="Item Name"
          reference={inamecfieldRef}
        ></CustomFormGroup>
        <CustomFormGroup
          controlId="form_Desc"
          label="Description"
          placeholder="Item Description"
          reference={descfieldRef}
        ></CustomFormGroup>
        <CustomFormGroup
          controlId="form_Quantity"
          label="Quantity"
          placeholder="Item Quantity"
          reference={qtfieldRef}
        ></CustomFormGroup>
        <CustomFormGroup
          controlId="form_Owner"
          label="Owner"
          placeholder="Numeric ID of owner"
          reference={ownerfieldRef}
        ></CustomFormGroup>
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
