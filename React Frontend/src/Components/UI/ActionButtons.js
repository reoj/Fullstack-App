import React, { Fragment } from "react";
import Button from "react-bootstrap/esm/Button";

function ActionButtons(props) {
  function editHandler() {
    props.clickForEdit();
  }
  function delHandler() {
    props.clickForDel();
  }

  return (
    <Fragment>
      <td>
        <Button
          onClick={editHandler}
          variant="warning"
          key={"edit_" + props.itemID}
        >
          Edit
        </Button>
      </td>
      <td>
        <Button
          onClick={delHandler}
          variant="danger"
          key={"del_" + props.itemID}
        >
          Delete
        </Button>
      </td>
    </Fragment>
  );
}

export default ActionButtons;
