import React, { useContext } from "react";
import Button from "react-bootstrap/esm/Button";
import { Link } from "react-router-dom/cjs/react-router-dom.min";
import ModalContext from "../../../Context/UI/modal-context";
import ActionButtons from "../../UI/ActionButtons";
import DeleteUser from "./DeleteUser";
import EditUser from "./EditUser";
import { editUserR } from "../../../Context/Users/user-redux-slice";

function UserSingle(props) {
  const cntx = useContext(ModalContext);
  const modalDispatch = cntx.setter;

  const single = {
    id: props.idn,
    name: props.name,
    cl: props.cl,
    email: props.email,
    phone: props.phone,
  };

  function onEditHandler() {
    modalDispatch({
      type: "OPEN",
      payload: {
        title: "Editing User",
        body: <EditUser item={single} />,
      },
    });
  }
  function onDeleteHandler() {
    modalDispatch({
      type: "OPEN",
      payload: {
        title: "Please confirm deletion of the following User",
        body: <DeleteUser item={single} />,
      },
    });
  }
  return (
    <tr>
      <td>{props.idn}</td>
      <td>{props.name}</td>
      <td>{props.cl}</td>
      <td>{props.email}</td>
      <td>{props.phone}</td>
      <td>
        <Link to={"/Filtered-items/" + props.idn}>
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
