import React, { useContext } from "react";
import Button from "react-bootstrap/esm/Button";
import { Link } from "react-router-dom/cjs/react-router-dom.min";
import ModalContext from "../../../Context/modal-context";
import ActionButtons from "../../UI/ActionButtons";
import DeleteUser from "./DeleteUser";
import EditUser from "./EditUser";

function UserSingle(props) {
  const context = useContext(ModalContext);
  const mc = context.setter;
  function onEditHandler(params) {
    mc({
      onDisplay: true,
      title: "Editing User",
      body: <EditUser item={{ id: props.idn, name: props.name, cl: props.cl}} />,
    });
  }
  function onDeleteHandler(params) {
    mc({
      onDisplay: true,
      title: "Please confirm deletion of the following User",
      body: <DeleteUser item={{ id: props.idn, name: props.name, cl: props.cl}} />,
    });
  }
  return (
    <tr>
      <td>{props.idn}</td>
      <td>{props.name}</td>
      <td>{props.cl}</td>
      <td>
        <Link to={"/Filtered-items/"+ props.idn}>
          <Button variant="primary" key={"itemsOf_" + props.itemID}>
            View
          </Button>
        </Link>
      </td>
      <ActionButtons
        clickForEdit={onEditHandler}
        clickForDel={onDeleteHandler}
        itemID={props.idn}
        forUser={true}
      />
    </tr>
  );
}

export default UserSingle;
