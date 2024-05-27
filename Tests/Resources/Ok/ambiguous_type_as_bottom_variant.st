language core;

extend with #ambiguous-type-as-bottom, #structural-subtyping, #variants;

fn main(n : Nat) -> <| a : Nat, b : Bool |> {
  return (fn (x : Nat) {
    return <| a = x |>
  })(n)
}
