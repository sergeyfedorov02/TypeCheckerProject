language core;
extend with
  #structural-subtyping,
  #variants,
  #records;

fn main(
  x : <| a : Nat, b : { c : Nat, d : Bool } |>
) -> <| a : Nat, b : { c : Nat } |> {
  return x
}