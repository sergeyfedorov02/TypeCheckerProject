language core;
extend with
  #structural-subtyping,
  #variants;

fn main(x : <| a : <| b : Bool |> |>) -> <| a : <| |> |> {
  return x
}