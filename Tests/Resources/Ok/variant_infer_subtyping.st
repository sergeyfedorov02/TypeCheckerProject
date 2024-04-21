language core;
extend with #references, #variants, #structural-subtyping, #lists;

fn main(n : Nat) -> <| x : Nat, y : Nat |> {
  return (fn (n : Nat) { return <| x =  0|> }) (0)
}