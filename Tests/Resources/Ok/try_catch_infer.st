language core;

extend with #exceptions, #exception-type-declaration;

exception type = Nat
fn main(n : Nat) -> Nat {
  return (fn (n : Nat) {return try { 0 } catch { x => x }}) (0)
}