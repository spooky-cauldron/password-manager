import { useState } from "react";
import Button from "@mui/material/Button";
import List from "@mui/material/List";
import DialogTitle from "@mui/material/DialogTitle";
import Dialog from "@mui/material/Dialog";
import { TextField } from "@mui/material";
import axios from "axios";

const passwordsUrl = "http://localhost:5000/passwords";

export default function NewPasswordModal(props: any) {
  const { open, setOpen, reloadPasswords } = props;
  const [name, setName] = useState("");
  const [value, setValue] = useState("");

  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setName("");
    setValue("");
    setOpen(false);
  };
  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>Add Password</DialogTitle>
      <TextField
        label="name"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <TextField
        style={{ width: 500 }}
        label="value"
        value={value}
        onChange={(e) => setValue(e.target.value)}
      />
      <Button
        onClick={() => {
          addPassword(name, value, reloadPasswords);
          handleClose();
        }}
      >
        Add
      </Button>
      <List sx={{ pt: 0 }}></List>
    </Dialog>
  );
}

function addPassword(name: string, value: string, handleResponse: any) {
  axios.post(passwordsUrl, { name, value }).then(() => handleResponse());
}
