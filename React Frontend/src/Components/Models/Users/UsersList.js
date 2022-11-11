import React, { Fragment, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import TableDisplayer from "../../UI/TableDisplayer";
import UserSingle from "./UserSingle";
import { fetchInitialState } from "../../../Context/Users/users-redux-actions";


function UsersList(props) {
  const usersList = useSelector((state) => state.root.user.value.list);
  const dsp = useDispatch()

  useEffect(() => {
    dsp(fetchInitialState());
  }, [dsp])

  const properties = ["ID", "Name", "Class","E-mail","Phone", "Items"];

  return (
    <Fragment>
      <TableDisplayer modelType="Users" colList={properties}>
        {usersList.map((u) => {
          return (
            <UserSingle
              key={"User__" + u.idUser}
              idn={u.idUser}
              name={u.name}
              cl = {u.userType}
              email = {u.email}
              phone = {u.phone}
            />
          );
        })}
      </TableDisplayer>
    </Fragment>
  );
}

export default UsersList;
