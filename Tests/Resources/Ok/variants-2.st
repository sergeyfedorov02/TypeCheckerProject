language core;
extend with
  #structural-subtyping,
  #variants;

fn main(x : <| a : <| |> |>) -> <| a : <| b : Bool |> |> {
  return x
}