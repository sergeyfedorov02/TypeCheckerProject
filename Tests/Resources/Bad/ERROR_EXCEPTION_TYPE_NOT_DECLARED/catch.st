language core;
extend with  #exceptions;

fn main(n : Nat) -> Nat {
	return try { 0 } catch { x => x }
}
