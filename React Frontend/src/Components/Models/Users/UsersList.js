import React, { Fragment } from "react";
import { useSelector } from "react-redux";
import TableDisplayer from "../../UI/TableDisplayer";
import UserSingle from "./UserSingle";


function UsersList(props) {
  const usersList = useSelector((state) => state.root.user.value.list);

  const properties = ["ID", "Name", "Class", "Items"];

  return (
    <Fragment>
      <TableDisplayer modelType="Users" colList={properties}>
        {usersList.map((u) => {
          return (
            <UserSingle
              key={"User__" + u.id.toString()}
              idn={u.id}
              name={u.name}
              cl = {u.utype}
            />
          );
        })}
      </TableDisplayer>
    </Fragment>
  );
}

export default UsersList;
